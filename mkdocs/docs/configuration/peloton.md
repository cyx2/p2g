# Peloton Settings

The Peloton Settings provide settings related to how P2G should fetch workouts from Peloton.

## Settings location

| Run Method | Location |
|------------|----------|
| Web UI     |  UI > Settings > Peloton Tab  |
| Windows Exe | UI > Settings > Peloton Tab |
| GitHubAction | Config Section in Workflow |
| Headless (Docker or Console) | Config section in `configuration.local.json` |

## File Configuration

```json
"Peloton": {
    "Email": "peloton@gmail.com",
    "Password": "peloton",
    "SessionId": "adsfd",
    "NumWorkoutsToDownload": 1,
    "ExcludeWorkoutTypes": [ "meditation" ]
  }
```

!!! warning
    Console or Docker Headless: Your username and password for Peloton and Garmin Connect are stored in clear text, which **is not secure**. Please be aware of the risks.
!!! success "WebUI version 3.3.0+: Credentials are stored **encrypted**."
!!! success "Windows Exe version 4.0.0+: Credentials are stored **encrypted**."
!!! success "GitHub Actions: Credentials are stored **encrypted**."

## Settings Overview

| Field      | Required | Default | Description |
|:-----------|:---------|:--------|:------------|
| Email | **yes** | `null` | Your Peloton email (for identification only) |
| Password | **yes** | `null` | Your Peloton password (for identification only) |
| SessionId | **yes** | `null` | Your Peloton sessionId - **REQUIRED** for authentication [Read more...](#peloton-session-id) |
| NumWorkoutsToDownload | no | 5 | The default number of workouts to download. See [choosing number of workouts to download](#choosing-number-of-workouts-to-download).  Set this to `0` if you would like P2G to prompt you each time for a number to download. |
| ExcludeWorkoutTypes | no | none | An array of workout types that you do not want P2G to download/convert/upload. [Read more...](#exclude-workout-types) |

## Choosing Number of Workouts To Download

When choosing the number of workouts P2G should download each polling cycle its important to keep your configured [Polling Interval](app.md) in mind. If, for example, your polling interval is set to hourly, then you may want to set `NumWorkoutsToDownload` to 4 or greater. This ensures if you did four 15min workouts during that hour they would all be captured.

Garmin is capable of rejecting duplicate workouts, so it is safe for P2G to attempt to sync a workout that may have been previously synced.

## Exclude Workout Types

If there are [Exercise Types](exercise-types.md) that you do not want P2G to sync, then you can specify those in the settings.

Some example use cases include:

1. You take a wide variety of Peloton classes, including meditation and you want to skip uploading meditation classes.
1. You want to avoid double-counting activities you already track directly on a Garmin device, such as outdoor running workouts.

The list of valid values are any [Exercise Type](exercise-types.md).

## Peloton Session Id

!!! danger "Required as of November 2025"
    **SessionId is required** - Peloton has deprecated their login API endpoint. Use SessionId for authentication.

### How to Get Your SessionId

1. Visit https://www.onepeloton.com in your web browser
2. Log in with your Peloton account
3. Open your browser's developer tools (F12 or right-click > Inspect)
4. Go to the **Application** tab (Chrome) or **Storage** tab (Firefox)
5. Navigate to **Cookies** > `https://www.onepeloton.com`
6. Find the cookie named `peloton_session_id`
7. Copy the **Value** of this cookie
8. Add it to your configuration file under `Peloton.SessionId`

### Alternative Method (Browser Extension)

You can also use a browser extension like "Cookie Editor" to easily view and copy cookies.

### Configuration

Add the SessionId to your configuration:

```json
"Peloton": {
    "Email": "peloton@gmail.com",
    "Password": "peloton",
    "SessionId": "your_session_id_here",
    "NumWorkoutsToDownload": 1
}
```

### Important Notes

- **SessionId expires**: You will need to manually update this token periodically when it expires (typically every few weeks or months)
- **Security**: SessionId is like a password and should never be shared
- **GitHub Actions**: Set SessionId as a secret similar to how you configure Email and Password
- **If SessionId expires**: Simply repeat the steps above to get a new SessionId and update your configuration

!!! danger 
    SessionId is like a password and should never be shared.
    Github action users should set SessionId as a secret similar to how you configure Email and Password.

!!! warning TODO: Better instructions and the ability to edit this from UI