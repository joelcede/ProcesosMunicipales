import { IRegularizacion } from "../../../Core/Models/IRegularizacion";

export interface IFichaRegularizacion {
  id: string; // Correspondiente a idRegularizacion
  regularizacion: IRegularizacion;
  vivienda: IRegularizacion;
  propietarios: IRegularizacion[];
}
