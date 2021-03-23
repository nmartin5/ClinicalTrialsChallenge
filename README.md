# Clinical Trials Challenge

## Local Development Setup

### Prerequisites

- Node.js [LTS version works fine](https://nodejs.org/en/download/)
- Visual Studio [Community or any version really](https://visualstudio.microsoft.com/downloads/)
- [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) (Included with Visual Studio)
- SendGrid API Key [free tier available](https://sendgrid.com/docs/for-developers/sending-email/api-getting-started/)


### Required Configuration

SendGrid API key is required for local execution. Easiest way to configure it is to setup user secrets [in visual studio](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows#json-structure-flattening-in-visual-studio) or [via the dotnet CLI](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows#set-a-secret).

Your secrets configuration must match the below structure:
```json
{
  "EmailOptions": {
    "ApiKey": "API_KEY_HERE",
    "AuthorEmail": "YOUR EMAIL ADDRESS",
    "AuthorName": "YOUR NAME"
  }
}
```

### Optional Configuration

By default, a local database will be lazily created once first needed by the runtime. This will be placed in the bin directory by default. If you would like to specify a different path, simply provide your `Sqlite` connection string with your user secrets as such:
```json
{
  "ConnectionStrings": {
    "ClinicalTrialsDbContext": "Data Source=C:\\Users\\TheBestUser\\Desktop\\clinical-trials.db"
  },
  "EmailOptions": {
    "ApiKey": "API_KEY_HERE",
    "AuthorEmail": "YOUR EMAIL ADDRESS",
    "AuthorName": "YOUR NAME"
  }
}
```

### Local Execution

- Running the UI
    ```bash
    cd .\ClinicalTrialsChallengeUi
    npm install
    ng serve
    ```

    This will serve up to http://localhost:4200 by default

- Running the API

    - Open `ClinicalTrialsChallenge.sln` in Visual Studio (required for Kestrel launch)
    - Run/Debug using the "IIS Express" profile
    - A browser will open with the openApi documentation to verify that all is well.
