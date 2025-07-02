
# Timezone Date Normalization API

A simple ASP.NET Core Web API that fixes issues with dates being saved incorrectly due to silent UTC conversion. It normalizes incoming `DateTime`, `DateOnly`, and nullable dates using the user's time zone, sent in an `X-Timezone` header.

---

## ✅ What It Does

- Accepts a time zone via `X-Timezone` (e.g. `Africa/Johannesburg`)
- Converts date/time inputs from the user's local time to UTC accurately for consistent storage
- Supports:
  - `DateTime`
  - `DateOnly`
  - Nullable versions of both
- Works globally via an action filter

---

## Why Date Normalization Solves the Bug

When a user sends a date or datetime from the frontend (e.g., Angular), the value is usually a **local time without explicit timezone context**. By default, .NET or the server may **interpret this incoming date as UTC**, which causes an incorrect shift in the stored value.

For example, a user in UTC+2 sending `2025-07-02T10:00:00` may have their time saved as `2025-07-02T10:00:00Z` (UTC), which is actually 2 hours earlier than intended, resulting in incorrect times on retrieval.

### What Normalization Does

Our API filter:

- **Reads the incoming datetime as if it is in the user’s local timezone** (using the `X-Timezone` header)
- **Converts that local time to UTC** correclty with no mismatch before storing or processing
- This preserves the exact moment in time the user intended, avoiding unintended timezone shifts

### Converting Dates Back to Local Time on the Client

The backend stores and returns dates in **standard UTC ISO 8601 format** (e.g., `2025-07-02T08:00:00Z`). To display dates correctly:

- The frontend (Angular) parses these UTC dates using JavaScript’s `Date` object.
- It converts the UTC date to the user’s local timezone automatically.
- Example in Angular/JavaScript:

```typescript
const utcDate = new Date("2025-07-02T08:00:00Z");
const localDateString = utcDate.toLocaleString(); // Displays in user's local timezone
```

## 🚀 Run and Test Locally with Docker

1. Build the Docker image:

```
docker build -t local-dotnet-utc
```
2. Run the container on port 5000:
```
docker run -p 5000:5000 local-dotnet-utc
```

3. Open in your browser:
```
http://localhost:5000/
```

4. To test APIs needing the X-Timezone header, use Postman or another API tool.

5. To stop the container, press Ctrl + C or run:
```
docker ps
docker stop <container_id>
```

## 💡 Why It Matters

ASP.NET APIs often treat dates as UTC unless told otherwise. This project ensures dates are saved and returned as the user expects — based on their actual time zone.

---

## 🧱 Built With

* ASP.NET Core 8
* Action filters
* Swagger (Swashbuckle)
* TimeZoneInfo

---

## 📄 License
MIT

---
Made by Troy Krause
