import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

interface ResumoResponse {
  dados: {
    saldoTotal: number;
    totalReceitas: number;
    totalDespesas: number;
  };
  mensagem: string;
  status: boolean;
}

interface PorCategoriaResponse {
  dados: {
    categoria: string;
    total: number;
  }[];
  mensagem: string;
  status: boolean;
}

@Injectable({ providedIn: 'root' })
export class RelatorioService {
  private ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) {}

  getResumo(dataInicio: string, dataFim: string) {
    let url = `${this.ApiUrl}/Relatorio/resumo`;
    if (dataInicio && dataFim) {
      url += `?dataInicio=${dataInicio}&dataFim=${dataFim}`;
    }
    return this.http.get<any>(url);
  }

  getPorCategoria(dataInicio: string, dataFim: string) {
    let url = `${this.ApiUrl}/Relatorio/por-categoria`;
    if (dataInicio && dataFim) {
      url += `?dataInicio=${dataInicio}&dataFim=${dataFim}`;
    }
    return this.http.get<any>(url);
  }
}
