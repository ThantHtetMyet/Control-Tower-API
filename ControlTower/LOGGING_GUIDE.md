# API Logging Guide

## ✅ File Logging is Now Configured!

### 📂 Log File Location:

```
ControlTower/ControlTower/Logs/
└── controltower-api-20251209.log  (daily rotating)
```

**Each day creates a new log file with the date in the filename.**

---

## 📊 What Gets Logged:

### All API requests and responses:
- HTTP requests (URL, method, status)
- Controller actions
- Database queries
- **MQTT messages** ← This is what we need!
- Errors and exceptions

### Log Format:
```
2025-12-09 11:15:30.123 [INF] CMReportFormController - [MQTT] Publishing request to: controltower/cm_reportform_signature_pdf/{id}
2025-12-09 11:15:45.456 [INF] CMReportFormController - [MQTT RECEIVED] Topic: controltower/cm_reportform_signature_pdf_status/{id}
2025-12-09 11:15:45.789 [ERR] CMReportFormController - Failed to generate final report PDF
```

---

## 🔍 How to Use Logs:

### 1. **After Submitting with Signatures:**

Open the log file:
```
ControlTower\ControlTower\Logs\controltower-api-{today}.log
```

### 2. **Search for Your Report ID:**

Example: `add86271-4f9a-41e8-913f-749abdc79a27`

You'll see the complete flow:
```
[INF] POST /api/CMReportForm/{id}/generate-final-report-pdf
[INF] [MQTT] Connected to broker, subscribing to: controltower/cm_reportform_signature_pdf_status/{id}
[INF] [MQTT] Publishing request to: controltower/cm_reportform_signature_pdf/{id}
[INF] [MQTT] Request published, waiting for response...
[INF] [MQTT RECEIVED] Topic: controltower/cm_reportform_signature_pdf_status/{id}
[INF] [MQTT PAYLOAD] {"report_id":"...","status":"completed","file_name":"..."}
[INF] PDF generation successful, File: ..., Size: ... bytes
[INF] Final report PDF generated and saved successfully
```

### 3. **Search for Errors:**

Look for `[ERR]` or `[FATAL]` tags:
```
[ERR] Failed to generate final report PDF
[ERR] Error fetching signature images
[ERR] MQTT connection failed
```

---

## 🎯 Log File Management:

### **Automatic Rotation:**
- New log file created daily
- Keeps last 7 days of logs
- Old logs automatically deleted

### **File Names:**
```
controltower-api-20251209.log  ← Today
controltower-api-20251208.log  ← Yesterday
controltower-api-20251207.log  ← 2 days ago
...
```

### **Maximum File Size:**
- Each log file can grow to ~10 MB
- Then rotates to a new file

---

## 🚀 Next Steps:

1. **Restore NuGet Packages:**
   ```bash
   # In Visual Studio
   Tools → NuGet Package Manager → Restore NuGet Packages
   
   # Or in terminal
   dotnet restore
   ```

2. **Rebuild the Application:**
   ```bash
   dotnet build
   ```

3. **Run the Application:**
   - Logs will start appearing in `Logs/` folder

4. **Test Signature Feature:**
   - Create report with signatures
   - Check log file for MQTT communication
   - Search for your report ID

---

## 📝 Log Levels:

- `[INF]` - Information (normal operations)
- `[WRN]` - Warning (potential issues)
- `[ERR]` - Error (something failed)
- `[FATAL]` - Fatal (application crash)

---

## 🔧 Troubleshooting:

### If log file doesn't appear:
- Check `ControlTower/ControlTower/Logs/` folder exists
- Verify application has write permissions
- Check Visual Studio Output for Serilog errors

### If logs are too verbose:
- Edit `Program.cs` and change `MinimumLevel.Information()` to `MinimumLevel.Warning()`

### If you need more detail:
- Change to `MinimumLevel.Debug()` or `MinimumLevel.Verbose()`

---

**Now you can easily review all API activity in the log files!** 📚

