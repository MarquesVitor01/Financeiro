import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RelatorioService } from '../../services/relatorio';
import { Chart, registerables } from 'chart.js';
Chart.register(...registerables);

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css'],
})
export class DashboardComponent implements OnInit {
  dataInicio: string = ''; // vazio no início
  dataFim: string = '';    // vazio no início

  saldoTotal: number = 0;
  totalReceitas: number = 0;
  totalDespesas: number = 0;
  categorias: { categoria: string; total: number }[] = [];

  constructor(private relatorioService: RelatorioService) {}

  ngOnInit(): void {
    this.carregarRelatorio();
  }

  carregarRelatorio() {
    this.relatorioService.getResumo(this.dataInicio, this.dataFim).subscribe((res) => {
      if (res.status) {
        this.saldoTotal = res.dados.saldoTotal;
        this.totalReceitas = res.dados.totalReceitas;
        this.totalDespesas = res.dados.totalDespesas;
      }
    });

    this.relatorioService.getPorCategoria(this.dataInicio, this.dataFim).subscribe((res) => {
      if (res.status) {
        this.categorias = res.dados;
        this.renderizarGrafico();
      }
    });
  }

  private grafico: Chart | null = null;

  renderizarGrafico() {
    const ctx = (document.getElementById('graficoCategorias') as HTMLCanvasElement).getContext('2d');
    if (!ctx) return;

    if (this.grafico) {
      this.grafico.destroy();
    }

    this.grafico = new Chart(ctx, {
      type: 'pie',
      data: {
        labels: this.categorias.map((c) => c.categoria),
        datasets: [
          {
            label: 'Total por Categoria',
            data: this.categorias.map((c) => c.total),
            backgroundColor: ['#6c63ff', '#31a3d1', '#ff6584', '#ffb84d', '#8bc34a'],
          },
        ],
      },
      options: {
        responsive: true,
        plugins: { legend: { position: 'bottom' } },
      },
    });
  }

  aplicarFiltro() {
    this.carregarRelatorio();
  }
}

