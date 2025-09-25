import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Transacoes } from '../../services/transacoes';
import { TransacaoListar } from '../../models/Transacao';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-detalhes',
  imports: [RouterModule, DatePipe],
  templateUrl: './detalhes.html',
  styleUrl: './detalhes.css'
})
export class Detalhes implements OnInit{
  transacao!: TransacaoListar;
  constructor(private transaService: Transacoes, private route: ActivatedRoute){}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'))
    this .transaService.GetTransacaoId(id).subscribe(response => {
      this.transacao = response.dados;
    })
  }

}
