
# Timezone Date Normalization API

A simple ASP.NET Core Web API that fixes issues with dates being saved incorrectly due to silent UTC conversion. It normalizes incoming `DateTime`, `DateOnly`, and nullable dates using the user's time zone, sent in an `X-Timezone` header.

---

## ✅ What It Does

- Accepts a time zone via `X-Timezone` (e.g. `Africa/Johannesburg`)
- Converts date/time inputs to match the user's local time
- Supports:
  - `DateTime`
  - `DateOnly`
  - Nullable versions of both
- Works globally via an action filter

---

## 🚀 How to Run

```
git clone https://github.com/your-username/timezone-date-normalization-api.git
cd timezone-date-normalization-api
dotnet run
````

Open Swagger UI:

```
https://localhost:5001/swagger
```

---

## 🧪 How to Test (Postman)

**URL**: `POST /api/test/test-dates`
**Headers**:

```
Content-Type: application/json
X-Timezone: Africa/Johannesburg
```

**Body**:

```json
{
  "eventTime": "2025-07-02T10:00:00",
  "eventDate": "2025-07-02"
}
```

---

## 💡 Why It Matters

ASP.NET APIs often treat dates as UTC unless told otherwise. This project ensures dates are saved and returned as the user expects — based on their actual time zone.

---

## 🧱 Built With

* ASP.NET Core 7
* Action filters
* Swagger (Swashbuckle)
* TimeZoneInfo

---

## 📄 License

MIT — free to use and modify.

```

---

Let me know if you'd like an even shorter version or want me to include a sample response.
```
