import http from "k6/http";
import { check, sleep } from "k6";

export const options = {
  vus: 10,
  duration: "20s",
  thresholds: {
    http_req_failed: ["rate<0.01"],      // <1% errors
    http_req_duration: ["p(95)<500"],    // p95 < 500ms (dev baseline)
  },
};

const BASE = __ENV.BASE_URL || "http://localhost:8080";

export default function () {
  // Readiness (API + DB)
  const ready = http.get(`${BASE}/health/ready`);
  check(ready, { "health/ready 200": (r) => r.status === 200 });

  // Dashboard
  const dash = http.get(`${BASE}/api/admin/dashboard`);
  check(dash, { "dashboard 200": (r) => r.status === 200 });

  // Admin lists (update paths if yours differ)
  const users = http.get(`${BASE}/api/admin/users?page=1&pageSize=20`);
  check(users, { "users 200": (r) => r.status === 200 });

  const teachers = http.get(`${BASE}/api/admin/teachers?page=1&pageSize=20`);
  check(teachers, { "teachers 200": (r) => r.status === 200 });

  const courses = http.get(`${BASE}/api/admin/courses?page=1&pageSize=20`);
  check(courses, { "courses 200": (r) => r.status === 200 });

  sleep(1);
}
