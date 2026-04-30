import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidenav',
  imports: [RouterModule, MatButtonModule],
  templateUrl: './sidenav.html',
  styleUrl: './sidenav.css',
  standalone: true,
})
export class Sidenav {}
