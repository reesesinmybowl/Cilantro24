# Route Type MVP (Strava Foundation)

This MVP sets up the Strava connection and activity browsing flow for a Next.js App Router app.

## What’s included

- Consumer-facing landing page with a **Connect with Strava** entry point.
- Strava OAuth login + callback flow.
- Server-side token exchange and refresh handling.
- Server-side session storage for Strava tokens (in-memory Map for MVP).
- Dashboard that loads and displays recent Strava activities.
- API route handlers for login, callback, and activities.

## Tech

- Next.js (App Router)
- TypeScript
- Route Handlers (`app/api/.../route.ts`)

## Environment variables

Create a `.env.local` file in `Cilantro26/`:

```bash
STRAVA_CLIENT_ID=your_strava_client_id
STRAVA_CLIENT_SECRET=your_strava_client_secret
STRAVA_REDIRECT_URI=http://localhost:3000/api/strava/callback
STRAVA_SCOPES=read,activity:read_all
```

> `STRAVA_CLIENT_SECRET` is only used server-side in route handlers/lib functions.

## Local setup

1. From the parent repository, move into this app folder:

```bash
cd Cilantro26
```

2. Install dependencies:

```bash
npm install
```

3. Start the development server:

```bash
npm run dev
```

4. Open `http://localhost:3000`.
5. Click **Connect with Strava**.
6. Authorize the app in Strava.
7. You should land on `/dashboard` and see recent activities.

## Notes for MVP token storage

- Tokens are stored in a server memory map keyed by a secure random session ID.
- The browser only receives an `HttpOnly` session cookie; no Strava tokens are exposed to client-side JavaScript.
- Because it is in-memory, sessions are cleared on server restart/redeploy. For production persistence, move sessions to a database or Redis.

## Manual verification checklist

- Login starts from `/api/strava/login` and redirects to Strava auth screen.
- Callback validates OAuth `state` and exchanges code for tokens.
- `/api/strava/activities` returns a list after login.
- Expired access token triggers refresh and still returns activities.
- Error states:
  - User denies authorization.
  - Missing/invalid state.
  - Missing session cookie.
