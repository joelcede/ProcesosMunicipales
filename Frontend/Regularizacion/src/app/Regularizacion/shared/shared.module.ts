import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { TextFieldModule } from '@angular/cdk/text-field';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { PdfFichaComponent } from './PDF/components/pdf-ficha/pdf-ficha.component';
import { UsuarioComponent } from './Usuario/Components/usuario/usuario.component';
import { ViviendaComponent } from './Vivienda/Components/vivienda/vivienda.component';
import { NgxLoadingButtonsModule } from 'ngx-loading-buttons';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatInputModule,
    MatToolbarModule,
    MatSlideToggleModule,
    TextFieldModule,
    MatButtonModule,
    MatDividerModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    NgxLoadingButtonsModule,
    ToastrModule.forRoot(),
    UsuarioComponent,
    ViviendaComponent,
    PdfFichaComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  exports: [
    UsuarioComponent,
    ViviendaComponent,
    PdfFichaComponent
  ]
})
export class SharedModule { }
