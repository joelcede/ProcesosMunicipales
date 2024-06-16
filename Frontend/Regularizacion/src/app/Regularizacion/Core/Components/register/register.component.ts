import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ViviendaComponent } from '../../../shared/Vivienda/Components/vivienda/vivienda.component';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { SharedModule } from '../../../shared/shared.module';
import { TipoUsuario } from '../../../shared/Usuario/Enums/TipoUsuario';
import { RegularizacionComponent } from '../regularizacion/regularizacion.component';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { SharedService } from '../../../shared/Servives/shared.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [
    FormsModule, MatFormFieldModule, MatInputModule,
    MatCardModule, MatSelectModule, ReactiveFormsModule,
    NgIf, NgFor, MatButtonModule, MatIconModule, MatDatepickerModule,
    MatNativeDateModule, MatDividerModule, MatDialogModule, ViviendaComponent,
    MatStepperModule, SharedModule, RegularizacionComponent
  ]
})
export class RegisterComponent implements OnInit {
  
  tipoCliente = TipoUsuario.Cliente;
  tipoPropietario = TipoUsuario.Propietario;
  tipoFamiliar = TipoUsuario.Familiar;

  @ViewChild('stepper') stepper!: MatStepper;
  firstFormGroup: FormGroup = this._formBuilder.group({ firstCtrl: [''] });
  secondFormGroup: FormGroup = this._formBuilder.group({ secondCtrl: [''] });
  thirdFormGroup: FormGroup = this._formBuilder.group({ thirdCtrl: [''] });
  fourthFormGroup: FormGroup = this._formBuilder.group({ fourthCtrl: [''] });
  fifthFormGroup: FormGroup = this._formBuilder.group({ fifthCtrl: [''] });
  currentStep: number = 0;
  constructor(private _formBuilder: FormBuilder, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.currentStep = 0;
  }

  onSelectionChange(event: StepperSelectionEvent): void {
    this.currentStep = event.selectedIndex;
    // Aquí puedes pasar la información al servicio compartido o directamente a otro componente
    this.sharedService.updateCurrentStep(this.currentStep);
  }
  toppings = new FormControl('');
  disableSelect = new FormControl(false);
  toppingList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato'];
  
}
