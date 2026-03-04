import { useEffect, useState } from "react";
import "./editCategoriaModal.css";

import {
  fetchCategoriasById,
  updateCategoria,
  deleteCategoria
} from "../../../services/categories.ts";

import type { CategoriaRequest } from "../../../services/categories.ts";

interface EditCategoriaModalProps {
  categoriaId: string | null;
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
}

export default function EditCategoriaModal({
  categoriaId,
  isOpen,
  onClose,
  onSuccess
}: EditCategoriaModalProps) {

  const [form, setForm] = useState<CategoriaRequest | null>(null);
  const [originalForm, setOriginalForm] = useState<CategoriaRequest | null>(null);

  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!isOpen || !categoriaId) return;

    fetchCategoriasById(categoriaId)
      .then(data => {
        const mapped: CategoriaRequest = {
          nome: data.nome
        };

        setForm(mapped);
        setOriginalForm(mapped);
      })
      .catch(() => setError("Erro ao carregar categoria"));
  }, [isOpen, categoriaId]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (!form) return;

    setForm({
      nome: e.target.value
    });
  };

  const isDirty = JSON.stringify(form) !== JSON.stringify(originalForm);

  const handleUpdate = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!form || !categoriaId) return;

    try {
      await updateCategoria(categoriaId, form);
      onClose();
      onSuccess?.();
    } catch {
      setError("Erro ao atualizar categoria");
    }
  };

  const handleDelete = async () => {
    if (!categoriaId) return;

    const confirmDelete = confirm("Tem certeza que deseja excluir?");
    if (!confirmDelete) return;

    try {
      await deleteCategoria(categoriaId);
      onClose();
      onSuccess?.();
    } catch {
      setError("Erro ao deletar categoria");
    }
  };

  if (!isOpen || !form) return null;

  return (
    <div className="modal-overlay">
      <div className="modal">

        <header>
          <h2>Editar Categoria</h2>
          <button onClick={onClose}>X</button>
        </header>

        <form onSubmit={handleUpdate}>

          <span>Nome</span>
          <input
            name="nome"
            value={form.nome}
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
              className={isDirty ? "save" : "save-disabled"}
            >
              Salvar
            </button>
          </div>

        </form>
      </div>
    </div>
  );
}