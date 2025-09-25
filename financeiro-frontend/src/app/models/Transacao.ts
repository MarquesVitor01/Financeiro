export interface TransacaoListar {
  id?: number;
  descricao: string;
  valor: number;
  categoriaId?: number;
  categoriaNome?: string;
  observacoes: string;
  dataCriacao: Date;
}
