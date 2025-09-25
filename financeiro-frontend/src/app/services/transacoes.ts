import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { TransacaoListar } from '../models/Transacao';
import { Response } from '../models/Response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Transacoes {

  ApiUrl = environment.UrlApi;

  constructor(private http : HttpClient){}

GetTransacoes(): Observable<Response<TransacaoListar[]>> {
  return this.http.get<Response<TransacaoListar[]>>(
    `${this.ApiUrl}/Transacao/ListarTransacoes`
  );
}

DeletarTranscao(id:number | undefined) : Observable<Response<TransacaoListar[]>>{
  return this.http.delete<Response<TransacaoListar[]>>(`${this.ApiUrl}/Transacao/ExcluirTransacao/${id} `);
}

CriarTransacao(transacao: TransacaoListar) : Observable<Response<TransacaoListar[]>>{
  return this.http.post<Response<TransacaoListar[]>>(`${this.ApiUrl}/Transacao/AdicionarTransacao`, transacao);
}

GetTransacaoId(id:number): Observable<Response<TransacaoListar>>{
  return this.http.get<Response<TransacaoListar>>(`${this.ApiUrl}/Transacao/BuscarTransacaoPorId/${id}`)
}

EditarTransacao(transacao: TransacaoListar): Observable<Response<TransacaoListar[]>>{
  return this.http.put<Response<TransacaoListar[]>>(`${this.ApiUrl}/Transacao/EditarTransacao`, transacao)
}
}
