import { useEffect, useState } from "react";
import "./ProductModal.css";

import {
  fetchProductById,
  updateProduct,
  deleteProduct
} from "../../../../src/services/products.ts";

import { fetchCategorias } from "../../../../src/services/categories.ts";

import type { ProductRequest } from "../../../../src/services/products.ts";
import type { CategoriaResponse } from "../../../../src/services/categories.ts";

interface EditProductModalProps {
  productId: string | null;
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
}

export default function EditProductModal({
  productId,
  isOpen,
  onClose,
  onSuccess
}: EditProductModalProps) {

  const [form, setForm] = useState<ProductRequest | null>(null);
  const [originalForm, setOriginalForm] = useState<ProductRequest | null>(null);

  const [categorias, setCategorias] = useState<CategoriaResponse[]>([]);
  const [error, setError] = useState<string | null>(null);

  // 🔹 Carregar produto + categorias
  useEffect(() => {
    if (!isOpen || !productId) return;

    Promise.all([
      fetchProductById(productId),
      fetchCategorias()
    ])
      .then(([produto, categoriasData]) => {

        const mapped: ProductRequest = {
          nome: produto.nome,
          descricao: produto.descricao,
          preco: produto.preco,
          quantidadeEmEstoque: produto.quantidadeEmEstoque,
          categoriaId: produto.categoria.id
        };

        setForm(mapped);
        setOriginalForm(mapped);
        setCategorias(categoriasData);
      })
      .catch(() => setError("Erro ao carregar produto"));
  }, [isOpen, productId]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    if (!form) return;

    const { name, value } = e.target;

    setForm(prev => ({
      ...prev!,
      [name]: name === "preco" || name === "quantidadeEmEstoque"
        ? Number(value)
        : value
    }));
  };

  // 🔹 Verifica se houve alteração
  const isDirty = JSON.stringify(form) !== JSON.stringify(originalForm);

  const handleUpdate = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!form || !productId) return;

    try {
      await updateProduct(productId, form);
      onClose();
      onSuccess?.();
    } catch {
      setError("Erro ao atualizar produto");
    }
  };

  const handleDelete = async () => {
    if (!productId) return;

    const confirmDelete = confirm("Tem certeza que deseja excluir?");
    if (!confirmDelete) return;

    try {
      await deleteProduct(productId);
      onClose();
      onSuccess?.();
    } catch {
      setError("Erro ao deletar produto");
    }
  };

  if (!isOpen || !form) return null;

  return (
    <div className="modal-overlay">
      <div className="modal">

        <header>
          <h2>Editar Produto</h2>
          <button onClick={onClose}>X</button>
        </header>

        <form onSubmit={handleUpdate}>

          <span>Nome</span>
          <input
            name="nome"
            value={form.nome}
            onChange={handleChange}
          />

          <span>Categoria</span>
          <select
            name="categoriaId"
            value={form.categoriaId}
            onChange={handleChange}
          >
            {categorias.map(cat => (
              <option key={cat.id} value={cat.id}>
                {cat.nome}
              </option>
            ))}
          </select>

          <span>Preço</span>
          <input
            type="number"
            name="preco"
            value={form.preco}
            onChange={handleChange}
          />

          <span>Descrição</span>
          <input
            name="descricao"
            value={form.descricao}
            onChange={handleChange}
          />

          <span>Estoque</span>
          <input
            type="number"
            name="quantidadeEmEstoque"
            value={form.quantidadeEmEstoque}
            onChange={handleChange}
          />

          <p className="error">{error}</p>

          <div className="actions">
            <button
              type="button"
              className="delete"
              onClick={handleDelete}
            >
              Excluir
            </button>

            <button
              type="submit"
              disabled={!isDirty}
              className="save"
            >
              Salvar
            </button>
          </div>

        </form>
      </div>
    </div>
  );
}