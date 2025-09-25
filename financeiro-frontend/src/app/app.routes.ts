import { Routes } from '@angular/router';
import { Cadastro } from './pages/cadastro/cadastro';
import { Home } from './pages/home/home';
import { Editar } from './pages/editar/editar';
import { Detalhes } from './pages/detalhes/detalhes';
import { CategoriasComponent } from './components/categorias/categorias';
import { DashboardComponent } from './components/dashboard/dashboard';

export const routes: Routes = [
  {
    path: 'cadastro',
    component: Cadastro,
  },
  {
    path: 'editar/:id',
    component: Editar,
  },
  {
    path: 'detalhes/:id',
    component: Detalhes,
  },
  {
    path: 'categorias', // 👈 nova rota
    component: CategoriasComponent,
  },
  { path: 'dashboard',
    component: DashboardComponent },
  {
    path: '',
    component: Home,
  },
];
