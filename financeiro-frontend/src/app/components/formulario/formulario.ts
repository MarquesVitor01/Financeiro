import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { formatDate, NgFor, NgIf } from '@angular/common';
import { TransacaoListar } from '../../models/Transacao';
import { CategoriasService } from '../../services/categorias';
import { Categoria } from '../../models/Categoria';

@Component({
  selector: 'app-formulario',
  standalone: true,
  imports: [RouterModule, ReactiveFormsModule, NgFor, NgIf],
  templateUrl: './formulario.html',
  styleUrls: ['./formulario.css'],
})
export class Formulario implements OnInit {
  @Input() btnAcao!: string;
  @Input() descTitulo!: string;
  @Input() dadosTransacao: TransacaoListar | null = null;
  @Output() onSubmit = new EventEmitter<TransacaoListar>();

  transacaoForm!: FormGroup;
  categorias: Categoria[] = [];
categoriasNomes: Categoria[] = [];
categoriasTipos: Categoria[] = [];

  constructor(private categoriaService: CategoriasService) {}

  ngOnInit(): void {
    this.transacaoForm = new FormGroup({
      id: new FormControl(this.dadosTransacao ? this.dadosTransacao.id : 0),
      descricao: new FormControl(this.dadosTransacao ? this.dadosTransacao.descricao : ''),
      valor: new FormControl(this.dadosTransacao ? this.dadosTransacao.valor : ''),
      categoriaId: new FormControl(this.dadosTransacao ? this.dadosTransacao.categoriaId : ''),
      categoriaNome: new FormControl(this.dadosTransacao ? this.dadosTransacao.categoriaNome : ''),
      observacoes: new FormControl(this.dadosTransacao ? this.dadosTransacao.observacoes : ''),
      dataCriacao: new FormControl(
      this.dadosTransacao
        ? formatDate(this.dadosTransacao.dataCriacao, 'yyyy-MM-dd', 'en')
        : ''
    ),
    });

    // Atualiza categoriaId ao mudar o nome da categoria
    this.transacaoForm.get('categoriaNome')?.valueChanges.subscribe(nome => {
      const cat = this.categorias.find(c => c.nome === nome);
      this.transacaoForm.get('categoriaId')?.setValue(cat ? cat.id : null);
    });

    this.carregarCategorias();
  }

carregarCategorias() {
  this.categoriaService.GetCategorias().subscribe(res => {
    if (res.dados) {
      this.categorias = res.dados;

      // Remover duplicados para Nome e Tipo
      this.categoriasNomes = [...new Map(res.dados.map(c => [c.nome, c])).values()];
      this.categoriasTipos = [...new Map(res.dados.map(c => [c.tipo, c])).values()];

      // Se houver dados de transação já preenchidos, ajusta o categoriaId
      if (this.dadosTransacao) {
        const cat = this.categorias.find(c => c.nome === this.dadosTransacao!.categoriaNome);
        this.transacaoForm.get('categoriaId')?.setValue(cat ? cat.id : null);
      }
    }
  });
}


  submit() {
    this.onSubmit.emit(this.transacaoForm.value);
  }
}
