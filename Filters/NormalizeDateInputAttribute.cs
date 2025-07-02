using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace DateInputNormalizer.Filters
{

    /// <summary>
    /// Action filter that normalizes all DateTime, Nullable<DateTime>, DateOnly, and Nullable<DateOnly>
    /// fields in incoming request models. It interprets them as being in the user's local timezone,
    /// preventing incorrect UTC shifts during deserialization.
    /// The timezone is specified via the "X-Timezone" header (IANA or Windows ID).
    /// </summary>
    public class NormalizeDateInputAttribute : ActionFilterAttribute
    {
        private const string TimeZoneHeader = "X-Timezone";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Attempt to get the user's timezone from the request header
            var tzId = context.HttpContext.Request.Headers[TimeZoneHeader].FirstOrDefault();

            if (string.IsNullOrEmpty(tzId) || TimeZoneInfo.FindSystemTimeZoneById(tzId) == null) throw new TimeZoneNotFoundException("Invalid or empty timezone.");

            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(tzId);

            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument != null)
                    NormalizeDates(argument, userTimeZone);
            }

            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Recursively normalizes DateTime and DateOnly properties in the model
        /// according to the user's timezone.
        /// </summary>
        private void NormalizeDates(object model, TimeZoneInfo userTimeZone)
        {
            if (model == null) return;

            var type = model.GetType();

            if (type.IsPrimitive || type == typeof(string)) return;

            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanRead || !prop.CanWrite) continue;

                var value = prop.GetValue(model);
                if (value == null) continue;

                // Normalize DateTime (non-nullable)
                if (prop.PropertyType == typeof(DateTime))
                {
                    var dt = (DateTime)value;
                    var corrected = ReinterpretAsUserTime(dt, userTimeZone);
                    prop.SetValue(model, corrected);
                }

                // Normalize Nullable<DateTime>
                else if (prop.PropertyType == typeof(DateTime?))
                {
                    var dt = (DateTime?)value;
                    if (dt.HasValue)
                    {
                        var corrected = ReinterpretAsUserTime(dt.Value, userTimeZone);
                        prop.SetValue(model, corrected);
                    }
                }

                // Normalize DateOnly (non-nullable)
                else if (prop.PropertyType == typeof(DateOnly))
                {
                    var corrected = ExtractLocalDateOnly((DateOnly)value, userTimeZone);
                    prop.SetValue(model, corrected);
                }

                // Normalize Nullable<DateOnly>
                else if (prop.PropertyType == typeof(DateOnly?))
                {
                    var dateOnly = (DateOnly?)value;
                    if (dateOnly.HasValue)
                    {
                        var corrected = ExtractLocalDateOnly(dateOnly.Value, userTimeZone);
                        prop.SetValue(model, corrected);
                    }
                }

                // If nested object, recurse into it
                else if (!prop.PropertyType.IsPrimitive &&
                         !prop.PropertyType.IsEnum &&
                         prop.PropertyType != typeof(string) &&
                         !typeof(IEnumerable<object>).IsAssignableFrom(prop.PropertyType))
                {
                    NormalizeDates(value, userTimeZone);
                }
            }
        }

        /// <summary>
        /// Reinterprets a DateTime as a local user time (avoids UTC misinterpretation).
        /// </summary>
        private DateTime ReinterpretAsUserTime(DateTime input, TimeZoneInfo userTimeZone)
        {
            // Treat input as local to the user's timezone, so mark as unspecified kind
            var unspecified = DateTime.SpecifyKind(input, DateTimeKind.Unspecified);

            // Convert from user local time to UTC
            var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(unspecified, userTimeZone);

            return utcDateTime;
        }

        /// <summary>
        /// Converts a DateOnly (which came from a DateTime) to the correct day in the user's timezone.
        /// This avoids issues like crossing over midnight into the wrong day.
        /// </summary>
        private DateOnly ExtractLocalDateOnly(DateOnly date, TimeZoneInfo userTimeZone)
        {
            // Assume midnight local (e.g., "2025-07-02T00:00:00")
            var asDateTime = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Unspecified);
            var adjusted = TimeZoneInfo.ConvertTime(asDateTime, userTimeZone);
            return DateOnly.FromDateTime(adjusted);
        }
    }



}
