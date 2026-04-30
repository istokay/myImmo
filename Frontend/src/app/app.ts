import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from '../components/header-component/header-component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { Sidenav } from '../components/sidenav/sidenav';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, MatSidenavModule, MatButtonModule, Sidenav],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('MyImmo');
}
