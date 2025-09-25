import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Response } from '../models/Response';
import { Categoria } from '../models/Categoria';

@Injectable({
  providedIn: 'root'
})
export class CategoriasService {
  private ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) {}

  GetCategorias(): Observable<Response<Categoria[]>> {
    return this.http.get<Response<Categoria[]>>(
      `${this.ApiUrl}/Categoria/ListarCategorias`
    );
  }

  GetCategoriaId(id: number): Observable<Response<Categoria>> {
    return this.http.get<Response<Categoria>>(
      `${this.ApiUrl}/Categoria/BuscarCategoriaPorId/${id}`
    );
  }

  CriarCategoria(categoria: Categoria): Observable<Response<Categoria[]>> {
    return this.http.post<Response<Categoria[]>>(
      `${this.ApiUrl}/Categoria/AdicionarCategoria`,
      categoria
    );
  }

  EditarCategoria(categoria: Categoria): Observable<Response<Categoria[]>> {
    return this.http.put<Response<Categoria[]>>(
      `${this.ApiUrl}/Categoria/EditarCategoria`,
      categoria
    );
  }

  DeletarCategoria(id: number): Observable<Response<Categoria[]>> {
    return this.http.delete<Response<Categoria[]>>(
      `${this.ApiUrl}/Categoria/ExcluirCategoria/${id}`
    );
  }
}
