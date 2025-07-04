import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
  stages: [
    { duration: '30s', target: 10000 },
    { duration: '3m', target: 10000 },
    { duration: '30s', target: 0 },
  ],
  thresholds: {
    http_req_duration: ['p(95)<500'],  // p95 < 500ms
    http_req_failed: ['rate<0.01'],     // menos del 1 % de errores
  },
};


export default function () {
    let res = http.get(`https://apitfg:8081/api/Eventos/empresa/1/mes/7/anno/2025`);
  check(res, {
    'status 200': (r) => r.status === 200,
  });
  sleep(1);
}
