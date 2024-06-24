import { AsyncPipe, CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatTabChangeEvent, MatTabsModule } from '@angular/material/tabs';
import { ViewComponent } from '../view/view.component';
import { RegisterComponent } from '../register/register.component';
import { SharedService } from '../../../shared/Servives/shared.service';
import { GraphicsComponent } from '../graphics/graphics.component';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
  standalone: true,
  imports: [CommonModule, MatTabsModule, ViewComponent, RegisterComponent, GraphicsComponent]
})
export class MainComponent {
  constructor(private sharedService: SharedService) { }
  onTabChange(event: MatTabChangeEvent) {
    this.sharedService.updateSelectedTabIndex(event.index);
  }
}
