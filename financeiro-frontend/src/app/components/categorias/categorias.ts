import { Component, OnInit } from '@angular/core';
import { CategoriasService } from '../../services/categorias';
import { Categoria } from '../../models/Categoria';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-categorias',
  standalone: true,
  imports: [NgFor, NgIf, FormsModule],
  templateUrl: './categorias.html',
  styleUrls: ['./categorias.css'],
})
export class CategoriasComponent implements OnInit {

  categorias: Categoria[] = [];
  novaCategoria: Categoria = { nome: '', tipo: '', ativo: true };
  editando: boolean = false;

  // Paginação
  paginaAtual: number = 1;
  itensPorPagina: number = 5; // quantidade de linhas por página

  constructor(private categoriaService: CategoriasService) {}

  ngOnInit(): void {
    this.carregarCategorias();
  }

  carregarCategorias(): void {
    this.categoriaService.GetCategorias().subscribe(res => {
      if (res.dados) {
        this.categorias = res.dados;
      }
    });
  }

  adicionarCategoria(): void {
    this.categoriaService.CriarCategoria(this.novaCategoria).subscribe(() => {
      this.carregarCategorias();
      this.novaCategoria = { nome: '', tipo: '', ativo: true };
      this.paginaAtual = 1; // reset página
    });
  }

  editarCategoria(categoria: Categoria): void {
    this.editando = true;
    this.novaCategoria = { ...categoria };
  }

  salvarEdicao(): void {
    if (this.novaCategoria.id) {
      this.categoriaService.EditarCategoria(this.novaCategoria).subscribe(() => {
        this.carregarCategorias();
        this.cancelarEdicao();
      });
    }
  }

  excluirCategoria(id: number | undefined): void {
    if (!id) return;
    this.categoriaService.DeletarCategoria(id).subscribe(() => {
      this.carregarCategorias();
    });
  }

  cancelarEdicao(): void {
    this.editando = false;
    this.novaCategoria = { nome: '', tipo: '', ativo: true };
  }

  // Métodos da paginação
  totalPaginas(): number {
    return Math.ceil(this.categorias.length / this.itensPorPagina);
  }

  categoriasPaginadas(): Categoria[] {
    const inicio = (this.paginaAtual - 1) * this.itensPorPagina;
    return this.categorias.slice(inicio, inicio + this.itensPorPagina);
  }

  irParaPagina(n: number): void {
    if (n < 1 || n > this.totalPaginas()) return;
    this.paginaAtual = n;
  }
}
