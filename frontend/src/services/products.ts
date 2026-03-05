
import api from './api';

// Request para criar/atualizar produto
export interface ProductRequest {
  nome: string;
  descricao: string;
  preco: number;
  quantidadeEmEstoque: number;
  categoriaId: string;
}

// Response do backend
export interface ProductResponse {
  id: string;
  nome: string;
  descricao: string;
  preco: number;
  quantidadeEmEstoque: number;
  categoria: {
    id: string;
    nome: string;
  };
}

// GET todos os produtos
export const fetchProducts = async (): Promise<ProductResponse[]> => {
  const { data } = await api.get('/produtos');
  return data;
};

// GET produtos filtrados com query params
export const fetchProductsFiltered = async (
  nome?: string,
  categoriaId?: string,
  estoqueMax?: number
): Promise<ProductResponse[]> => {
  const { data } = await api.get('/produtos', {
    params: {
      nome,
      categoriaId,
      estoqueMax,
    },
  });
  return data;
};

// GET produto por ID
export const fetchProductById = async (id: string): Promise<ProductResponse> => {
  const { data } = await api.get(`/produtos/${id}`);
  return data;
};

// POST criar produto
export const createProduct = async (product: ProductRequest): Promise<ProductResponse> => {
  const { data } = await api.post('/produtos', product);
  return data;
};

// PUT atualizar produto
export const updateProduct = async (id: string, product: ProductRequest): Promise<ProductResponse> => {
  const { data } = await api.put(`/produtos/${id}`, product);
  return data;
};

// DELETE produto
export const deleteProduct = async (id: string): Promise<void> => {
  await api.delete(`/produtos/${id}`);
};