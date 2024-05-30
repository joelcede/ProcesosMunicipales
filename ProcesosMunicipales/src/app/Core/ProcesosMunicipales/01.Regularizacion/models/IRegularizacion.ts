export enum EstadosType {
  None = 0,
  PorHacer = 1,
  EnEspera = 2,
  SubSanacion = 3,
  Aprobada = 4,
  TerminadaPendiente = 5,
  Terminada = 6,
  Negada = 7,
}

export interface IRegularizacion {
  id: string;
  idVivienda: string;
  numeroExpediente: string;
  valorRegularizacion: number;
  anticipo: number;
  valorPendiente: number;
  pagado: boolean;
  estado: EstadosType;
  fechaRegistro: Date;
  fechaInsercion: Date;
  fechaActualizacion: Date;
}
