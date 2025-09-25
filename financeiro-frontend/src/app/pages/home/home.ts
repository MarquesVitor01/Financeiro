import { Component, OnInit } from '@angular/core';
import { Transacoes } from '../../services/transacoes';
import { TransacaoListar } from '../../models/Transacao';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgFor, NgIf, FormsModule, RouterLink],
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class Home implements OnInit {

  transacoes: TransacaoListar[] = [];

  // Filtros
  filtroDescricao: string = '';
  filtroId: number | null = null;
  filtroDataInicio: string = '';
  filtroDataFim: string = '';

  // Paginação
  paginaAtual: number = 1;
  itensPorPagina: number = 5;

  constructor(private transacoesService: Transacoes) {}

  ngOnInit(): void {
    this.carregarTransacoes();
  }

  carregarTransacoes(): void {
    this.transacoesService.GetTransacoes().subscribe(res => {
      if (res.dados) {
        this.transacoes = res.dados;
      }
    });
  }

  deletar(id: number | undefined): void {
    if (!id) return;
    this.transacoesService.DeletarTranscao(id).subscribe(() => {
      this.carregarTransacoes();
    });
  }

  // Filtragem avançada
  transacoesFiltradas(): TransacaoListar[] {
    return this.transacoes.filter(t => {
      let match = true;

      if (this.filtroDescricao) {
        match = match && t.descricao.toLowerCase().includes(this.filtroDescricao.toLowerCase());
      }

      if (this.filtroId !== null && this.filtroId !== undefined) {
        match = match && t.id === this.filtroId;
      }

      if (this.filtroDataInicio) {
        const dataInicio = new Date(this.filtroDataInicio);
        const dataTransacao = new Date(t.dataCriacao);
        match = match && dataTransacao >= dataInicio;
      }

      if (this.filtroDataFim) {
        const dataFim = new Date(this.filtroDataFim);
        const dataTransacao = new Date(t.dataCriacao);
        match = match && dataTransacao <= dataFim;
      }

      return match;
    });
  }

  // Paginação
  totalPaginas(): number {
    return Math.ceil(this.transacoesFiltradas().length / this.itensPorPagina);
  }

  transacoesPaginadas(): TransacaoListar[] {
    const inicio = (this.paginaAtual - 1) * this.itensPorPagina;
    return this.transacoesFiltradas().slice(inicio, inicio + this.itensPorPagina);
  }

  irParaPagina(n: number): void {
    if (n < 1 || n > this.totalPaginas()) return;
    this.paginaAtual = n;
  }

  // Dashboard cards
  totalReceitas(): number {
    return this.transacoes.filter(t => t.valor > 0).length;
  }

  totalDespesas(): number {
    return this.transacoes.filter(t => t.valor < 0).length;
  }

  totalTransacoes(): number {
    return this.transacoes.length;
  }

  resetPag(): void {
    this.paginaAtual = 1;
  }
}
