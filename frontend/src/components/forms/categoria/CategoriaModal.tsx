import { useState } from "react";
import "./CategoriaModal.css";

import { createCategoria } from "../../../services/categories.ts";

import type { CategoriaRequest } from "../../../services/categories.ts";

interface CategoriaModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
}

export default function CategoriaModal({ isOpen, onClose, onSuccess }: CategoriaModalProps) {

  const [form, setForm] = useState<CategoriaRequest>({
    nome: ""
  });

  const [error, setError] = useState<string | null>(null);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { value } = e.target;

    setForm({
      nome: value
    });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      await createCategoria(form);
      onClose();
      onSuccess?.();
    } catch {
      setError("Erro ao criar categoria");
    }
  };

  if (!isOpen) return null;

  return (
    <div className="modal-overlay">
      <div className="modal">

        <header>
          <h2>Criar Categoria</h2>
          <button onClick={onClose}>X</button>
        </header>

        <form onSubmit={handleSubmit}>

          <span>Nome</span>
          <input
            name="nome"
            placeholder="Nome da categoria"
            value={form.nome}
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