import { NgDompurifySanitizer } from "@tinkoff/ng-dompurify";
import { TuiRootModule, TuiDialogModule, TuiAlertModule, TUI_SANITIZER, TuiButtonModule, TuiSvgModule } from "@taiga-ui/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { UsuarioComponentComponent } from './Shared/components/Usuarios/usuario-component/usuario-component.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TuiInputModule, TuiInputNumberModule, TuiUnmaskHandlerModule } from '@taiga-ui/kit';
import { MaskitoModule } from '@maskito/angular';
import { TuiTableFiltersModule, TuiTableModule } from "@taiga-ui/addon-table";
import { TuiLetModule } from "@taiga-ui/cdk";
import { HttpClientModule } from "@angular/common/http";
import { AddEditButtonUserComponent } from './Shared/components/Usuarios/add-edit-button-user/add-edit-button-user.component';

@NgModule({
  declarations: [
    AppComponent,
    UsuarioComponentComponent,
    AddEditButtonUserComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    TuiRootModule,
    TuiDialogModule,
    TuiAlertModule,
    TuiInputModule,
    TuiInputNumberModule,
    TuiUnmaskHandlerModule,
    MaskitoModule,
    TuiButtonModule,
    TuiTableModule,
    TuiSvgModule,
    TuiLetModule,
    TuiTableFiltersModule

  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [{ provide: TUI_SANITIZER, useClass: NgDompurifySanitizer }],
  bootstrap: [AppComponent]
})
export class AppModule { }
