import { Component, OnInit } from '@angular/core';
import { Formulario } from "../../components/formulario/formulario";
import { Transacoes } from '../../services/transacoes';
import { ActivatedRoute, Router } from '@angular/router';
import { TransacaoListar } from '../../models/Transacao';
import { CommonModule } from '@angular/common';
import { response } from 'express';

@Component({
  selector: 'app-editar',
  imports: [Formulario, CommonModule],
  templateUrl: './editar.html',
  styleUrl: './editar.css'
})
export class Editar implements OnInit {
  btnAcao = "Editar";
  descTitulo = "Editar transaçao"
  transacao!: TransacaoListar;

  constructor(private transacaoService: Transacoes, private router: Router, private route: ActivatedRoute){
  }
  ngOnInit(){
    const id = Number(this.route.snapshot.paramMap.get("id"))

    this.transacaoService.GetTransacaoId(id).subscribe(response => {
      this.transacao = response.dados
    })
  }
  editarTransacao(transacao:TransacaoListar){
    this.transacaoService.EditarTransacao(transacao).subscribe(response => {
      this.router.navigate(['/']);
    })
  }
}
