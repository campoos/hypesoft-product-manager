// src/services/products.ts
import api from './api';

// src/services/products.d.ts

// Request para criar/atualizar produto
export interface CategoriaRequest {
  nome: string;
}

// Response do backend
export interface CategoriaResponse {
  id: string;
  nome: string;
}

export const fetchCategorias = async (): Promise<CategoriaResponse[]> => {
  const { data } = await api.get('/categorias');
  return data;
};

// GET produto por ID
export const fetchCategoriasById = async (id: string): Promise<CategoriaResponse> => {
  const { data } = await api.get(`/categorias/${id}`);
  return data;
};

// POST criar produto
export const createCategoria = async (categoria: CategoriaRequest): Promise<CategoriaResponse> => {
  const { data } = await api.post('/categorias', categoria);
  return data;
};

// PUT atualizar produto
export const updateCategoria = async (id: string, categoria: CategoriaRequest): Promise<CategoriaResponse> => {
  const { data } = await api.put(`/categorias/${id}`, categoria);
  return data;
};

// DELETE produto
export const deleteCategoria = async (id: string): Promise<void> => {
  await api.delete(`/categorias/${id}`);
};