import { useState, useEffect } from 'react';
import { fetchDashboard } from '../services/dashboards';
import type { DashboardResponse } from '../../src/services/dashboards.ts'

export function useDashboardData() {
  const [dashboardData, setDashboardData] = useState<DashboardResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchDashboard()
      .then(data => setDashboardData(data))
      .catch(err => setError('Erro ao carregar dashboard:' + {err}))
      .finally(() => setLoading(false));
  }, []);

  return { dashboardData, loading, error };
}