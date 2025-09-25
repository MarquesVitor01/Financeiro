import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TransacaoListar } from '../../models/Transacao';
import { Transacoes } from '../../services/transacoes';
import { Formulario } from '../../components/formulario/formulario';

@Component({
  selector: 'app-cadastro',
  imports: [Formulario],
  templateUrl: './cadastro.html',
  styleUrls: ['./cadastro.css']
})
export class Cadastro {
  btnAcao = "Cadastrar";
  descTitulo = "Cadastrar transação";

  constructor(private transacaoService: Transacoes, private router: Router) {}

  // Recebe o objeto completo do formulário, já com categoriaId atualizado
  criarTransacao(transacao: TransacaoListar) {
    this.transacaoService.CriarTransacao(transacao).subscribe(() => {
      this.router.navigate(['/']);
    });
  }
}
