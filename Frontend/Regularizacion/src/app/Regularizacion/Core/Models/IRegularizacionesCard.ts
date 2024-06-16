export interface IRegularizacionesCard {
  idRegularizacion: string;
  idVivienda: string;
  nombreTramite: string;
  estadoRegularizacion: string;
  valorRegularizacion: number;
  imagenPrincipal: string; // byte array needs to be converted to a string for display purposes
  nombrePropietario: string;
  celular: string;
  codigoCatastral: string;
}
