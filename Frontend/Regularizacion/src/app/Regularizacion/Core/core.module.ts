import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatPaginatorModule } from '@angular/material/paginator';

//componentes de terceros
import { MatTabsModule } from '@angular/material/tabs';
import { MainComponent } from './Components/main/main.component';
import { ViewComponent } from './Components/view/view.component';
import { MatInputModule } from '@angular/material/input';
import { TextFieldModule } from '@angular/cdk/text-field';
import { RegisterComponent } from './Components/register/register.component';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { MatDialogModule } from '@angular/material/dialog';
import { MatStepperModule } from '@angular/material/stepper';
import { SharedModule } from '../shared/shared.module';
import { RegularizacionComponent } from './Components/regularizacion/regularizacion.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatExpansionModule } from '@angular/material/expansion';
@NgModule({
  declarations: [
  
  ],
  imports: [
    CommonModule,
    MatTabsModule,
    MatInputModule,
    TextFieldModule,
    MatCardModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatDatepickerModule,
    MatDividerModule,
    MatDialogModule,
    MatStepperModule,
    MatGridListModule,
    MatExpansionModule,
    MatPaginatorModule,
    //SharedModule,
    MainComponent,
    ViewComponent,
    RegisterComponent,
    RegularizacionComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  exports: [
    MainComponent,
    ViewComponent,
    RegisterComponent,
    RegularizacionComponent
  ]
})
export class CoreModule { }
