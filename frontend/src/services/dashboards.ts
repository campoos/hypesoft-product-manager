import api from './api';

// Response do backend para o Dashboard
export interface DashboardResponse {
  totalProdutos: number;
  valorTotalEstoque: number;
  produtosComEstoqueBaixo: number;
}

// GET dados do Dashboard
export const fetchDashboard = async (): Promise<DashboardResponse> => {
  const { data } = await api.get<DashboardResponse>('/dashboards');
  return data;
};