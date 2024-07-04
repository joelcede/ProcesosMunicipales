import { Component, Inject, OnInit } from '@angular/core';
import { IVivienda } from '../../Models/IVivienda';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ViviendaService } from '../../Services/vivienda.service';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { DataService } from '../../Services/data.service';
import { MaskitoOptions } from '@maskito/core';
import { MaskitoDirective } from '@maskito/angular';
import { MatButtonModule } from '@angular/material/button';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { NgFor, NgIf } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { TextFieldModule } from '@angular/cdk/text-field';
import { MatDividerModule } from '@angular/material/divider';
import { MatGridListModule } from '@angular/material/grid-list';

@Component({
  selector: 'app-viviendacmv',
  templateUrl: './viviendacmv.component.html',
  styleUrls: ['./viviendacmv.component.css'],
  standalone: true,
  imports: [MatFormFieldModule, MatSelectModule, ReactiveFormsModule,
    NgIf, NgFor, MatIconModule, MatInputModule, TextFieldModule, MatDividerModule, FormsModule,
    MaskitoDirective, MatButtonModule, MatDialogModule, ToastrModule, MatSnackBarModule, MatGridListModule]
})
export class ViviendacmvComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: IVivienda,
    public dialogRef: MatDialogRef<ViviendacmvComponent>,
    private viviendaService: ViviendaService,
    private toastr: ToastrService,
    private dataService: DataService,
    private snackBar: MatSnackBar,) { }

  ngOnInit() {
    this.getVivienda();
  }

  viviendaForm = new FormGroup({
    id: new FormControl('00000000-0000-0000-0000-000000000000'),
    direccion: new FormControl('', [Validators.required]),
    codigoCatastral: new FormControl(''),
    telefono: new FormControl(''),
    coordenadas: new FormControl(''),
    fechaCreacion: new FormControl(new Date()),
    fechaActualizacion: new FormControl(new Date()),
    imagen: new FormControl('', [Validators.required])
  });
  codigoCastastral = new FormControl('', [Validators.required]);
  readonly digitos10: MaskitoOptions = {
    mask: [/\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, '-', /\d/, '-', /\d/, '-', /\d/, '-', /\d/],
  };
  readonly digitos10Cel: MaskitoOptions = {
    mask: [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/],
  };
  selectedFiles?: FileList;
  textoImagen = 'Subir Imagen';
  colorSubida = 'warn';
  loading = false;
  imagen: string;

  onFileSelected(event: any) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      let fileName = file.name;
      reader.readAsDataURL(file);

      reader.onload = (e: any) => {
        const base64String = (reader.result as string).split(',')[1]; // Extraer solo la parte base64
        this.viviendaForm.patchValue({
          imagen: base64String
        });
        this.snackBar.open('Imagen subida correctamente', 'Cerrar', {
          duration: 3000,
        });
        this.imagen = `data:image/png;base64,${base64String}`
        if (fileName.length > 15) {
          fileName = fileName.substring(0, 15) + '...';
          this.textoImagen = fileName;
        }
        this.colorSubida = 'primary';
      };

    }
  }
  getErrorMessage() {
    if (this.codigoCastastral.hasError('required')) {
      return 'Ingresa un codigo Catastral';
    }
    return '';
  }
  allFormsValid(): boolean {
    return this.viviendaForm.valid || this.loading;
  }
  getVivienda() {
    this.viviendaService.getByIdVivienda(this.data.id).subscribe({
      next: (data: IVivienda) => {
        this.viviendaForm.patchValue({
          id: data.id,
          direccion: data.direccion,
          codigoCatastral: data.codigoCatastral,
          telefono: data.telefono,
          coordenadas: data.coordenadas,
          imagen: data.imagen
        })
        this.imagen = `data:image/png;base64,${data.imagen}`
        this.colorSubida = 'primary';
        //this.textoImagen = data.imagen;
        //this.colorSubida = 'warn';
      },
      error: () => { },
      complete: () => { }
    });
  }
  get viviendaF(): IVivienda {
    return this.viviendaForm.value as unknown as IVivienda;
  }
  putVivienda() {
    this.viviendaService.updateVivienda(this.data.id, this.viviendaF).subscribe({
      next: () => {
        this.toastr.success('Se actualizo la vivienda', 'Actualizacion');
      },
      error: () => { this.toastr.warning('Ocurrio un error.', 'Error'); },
      complete: () => {
        this.dataService.refreshTable() 
      }

    })
  }
}
