import { Routes } from '@angular/router';
import { RealEstateComponent } from '../components/real-estate-component/real-estate-component';
import { HomeComponent } from '../components/home-component/home-component';
import { RealEstateDetails } from '../components/real-estate-component/real-estate-details/real-estate-details';

export const routes: Routes = [
  {
    path: 'immos',
    component: RealEstateComponent,
  },
  {
    path: `realestate/:id`,
    component: RealEstateDetails,
  },
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: '**',
    component: HomeComponent,
  },
];
