import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
  stages: [
    { duration: '2m', target: 1000 }, // ramp-up hasta 1 000 usuarios
    { duration: '10m', target: 1000 },// mantener
    { duration: '2m', target: 0 },    // ramp-down
  ],
  thresholds: {
    http_req_duration: ['p(95)<500'],  // p95 < 500ms
    http_req_failed: ['rate<0.01'],     // menos del 1 % de errores
  },
};

export default function () {
  let res = http.get('https://localhost:8081/api/Eventos/empresa/2/mes/6/anno/2025');
  check(res, {
    'status 200': (r) => r.status === 200,
  });
  sleep(1);
}
