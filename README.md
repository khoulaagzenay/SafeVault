# SafeVault — Secure Web App

This repository collects the final deliverable for the SafeVault secure coding project:
- Input validation & SQL injection prevention
- Authentication & Authorization (RBAC)
- Debugging & resolving vulnerabilities (SQLi, XSS)
- Automated security tests

## Contents
- `app/` — application source (controllers, routes, middleware)
- `db/` — database helpers (parameterized queries)
- `auth/` — authentication + RBAC middleware
- `tests/` — automated tests (Jest + SuperTest)
- `SECURITY_SUMMARY.md` — brief summary of vulnerabilities and fixes
- `README.md` — this file

## How to run (Node.js example)
1. Install dependencies:
   ```bash
   npm install
