import { Routes } from '@angular/router';
import { RealEstateComponent } from '../components/real-estate-component/real-estate-component';
import { HomeComponent } from '../components/home-component/home-component';

export const routes: Routes = [
    {
        path: 'immos',
        component: RealEstateComponent
    },
    {
        path: '',
        component: HomeComponent
    }, 
    {
        path: '**',
        component: HomeComponent
    }
];
