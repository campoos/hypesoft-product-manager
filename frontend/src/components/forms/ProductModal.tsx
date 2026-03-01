import { useEffect, useState } from "react";
import "./ProductModal.css";

import { createProduct } from "../../../src/services/products.ts";
import { fetchCategorias } from "../../../src/services/categories.ts";

import type { ProductRequest } from "../../../src/services/products.tsx";
import type { CategoriaResponse } from "../../../src/services/categories.ts";

interface ProductModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
}

export default function ProductModal({ isOpen, onClose, onSuccess }: ProductModalProps) {

    const [form, setForm] = useState<ProductRequest>({
        nome: "",
        descricao: "",
        preco: 0,
        quantidadeEmEstoque: 0,
        categoriaId: "",
    });

    const [categorias, setCategorias] = useState<CategoriaResponse[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!isOpen) return;

        fetchCategorias()
        .then(data => setCategorias(data))
        .catch(() => setError("Erro ao carregar categorias"));
    }, [isOpen]);

      const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;

    setForm(prev => ({
      ...prev,
        [name]: name === "preco" || name === "quantidadeEmEstoque"
            ? Number(value)
            : value
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
        await createProduct(form);
        onClose();
        onSuccess?.();
        } catch (err) {
        setError("Erro ao criar produto");
        }
    };

    if (!isOpen) return null;

    return (
        <div className="modal-overlay">
        <div className="modal">

            <header>
            <h2>Criar Produto</h2>
            <button onClick={onClose}>X</button>
            </header>

            <form onSubmit={handleSubmit}>

            <span>Nome</span>
            <input
                name="nome"
                placeholder="Nome"
                value={form.nome}
                onChange={handleChange}
                required
            />

            <span>Categoria</span>
            <select
                name="categoriaId"
                value={form.categoriaId}
                onChange={handleChange}
                required
            >
                <option value="">Selecione uma categoria</option>

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
                placeholder="Preço"
                value={form.preco}
                onChange={handleChange}
                required
            />

            <span>Descrição</span>
            <input
                name="descricao"
                placeholder="Descrição"
                value={form.descricao}
                onChange={handleChange}
                required
            />

            <span>Estoque</span>
            <input
                type="number"
                name="quantidadeEmEstoque"
                placeholder="Estoque"
                value={form.quantidadeEmEstoque}
                onChange={handleChange}
                required
            />

            <p className="error">{error}</p>

            <button type="submit">Criar</button>
            </form>

        </div>
        </div>
    );
}