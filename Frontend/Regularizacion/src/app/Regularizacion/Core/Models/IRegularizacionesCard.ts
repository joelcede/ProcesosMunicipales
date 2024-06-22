export interface IRegularizacionesCard {
  idRegularizacion: string;
  idVivienda: string;
  nombreTramite: string;
  estadoRegularizacion: string;
  numRegularizacion: number;
  valorRegularizacion: number;
  imagenPrincipal: string; // byte array needs to be converted to a string for display purposes
  nombrePropietario: string;
  celular: string;
  dni: string;
  codigoCatastral: string;
}
