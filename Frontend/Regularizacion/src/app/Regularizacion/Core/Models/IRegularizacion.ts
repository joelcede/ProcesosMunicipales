export interface IRegularizacion {
  id: string;
  idVivienda: string;
  numeroExpediente: string;
  valorRegularizacion: number;
  anticipo: number;
  valorPendiente: number;
  pagado: boolean;
  estado: number;
  fechaRegistro: Date;
  fechaInsercion: Date;
  fechaActualizacion: Date;
  correo: string;
  contrasena: string;
}
