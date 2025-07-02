
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
---

## ✅ Date Normalization Test Cases

### ❌ Without Filter (Bug)

**Request:**

```
{
  "eventTime": "2025-07-02T10:00:00",
  "eventDate": "2025-07-02"
}
```
- Expected Timezone: Africa/Johannesburg (UTC+2)

**Response:**
```
{
  "serverReceived": {
    "eventTime": "2025-07-02T10:00:00",
    "eventDate": "2025-07-02"
  },
  "serverNow": "2025-07-02T21:31:02.3675393+00:00",
  "serverUtcNow": "2025-07-02T21:31:02.3675417Z"
}
```
**Problem:** 
The server stores and returns the time exactly as sent (10:00), but interprets it as UTC instead of the user's local time. This causes a 2-hour shift in meaning — it’s actually treated as 12:00 in Johannesburg, not the intended 10:00.

### ✅ With Filter (Correct Behavior)
**Same Request:**
```
{
  "eventTime": "2025-07-02T10:00:00",
  "eventDate": "2025-07-02"
}

//Note
Header: X-Timezone: Africa/Johannesburg
```

**Response:**
```
{
  "serverReceived": {
    "eventTime": "2025-07-02T08:00:00Z",
    "eventDate": "2025-07-02"
  },
  "serverNow": "2025-07-02T21:28:22.4007827+00:00",
  "serverUtcNow": "2025-07-02T21:28:22.4007835Z"
}
```

**Explanation:**
The filter interprets the 10:00 input as being in Africa/Johannesburg, and converts it to UTC 08:00 for storage. This preserves the exact intended moment across timezones. When sent back to the user, the frontend can safely convert it back to 10:00 local time.

---
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

## Time Zones

| Region               | IANA Timezone ID       | UTC Offset |
|----------------------|------------------------|------------|
| South Africa         | Africa/Johannesburg    | UTC+2      |
| United Kingdom       | Europe/London          | UTC+0 / +1 (DST) |
| United States (EST)  | America/New_York       | UTC-5 / -4 (DST) |
| United States (PST)  | America/Los_Angeles    | UTC-8 / -7 (DST) |
| Central Europe       | Europe/Berlin          | UTC+1 / +2 (DST) |
| India                | Asia/Kolkata           | UTC+5:30   |
| Japan                | Asia/Tokyo             | UTC+9      |
| Australia (Sydney)   | Australia/Sydney       | UTC+10 / +11 (DST) |
| Brazil (São Paulo)   | America/Sao_Paulo      | UTC-3      |
| UAE                  | Asia/Dubai             | UTC+4      |

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
