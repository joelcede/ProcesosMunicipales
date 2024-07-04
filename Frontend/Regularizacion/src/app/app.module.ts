import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//componentes propios
import { CoreModule } from './Regularizacion/Core/core.module';
import { SharedModule } from './Regularizacion/shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ViviendaModule } from './Viviendas/vivienda/vivienda.module';
import { UsuarioModule } from './Usuarios/usuario/usuario.module';
import { UsuariocmvComponent } from './Usuarios/Components/usuariocmv/usuariocmv.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CoreModule,
    SharedModule,
    ReactiveFormsModule,
    ViviendaModule,
    UsuarioModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
