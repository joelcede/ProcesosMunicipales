import { CommonModule, NgFor } from '@angular/common';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Chart, ChartType } from 'chart.js/auto';
import { RegularizacionService } from '../../Services/regularizacion.service';
import { HttpErrorResponse } from '@angular/common/http';
import { IGraphGananciaMesReg, IGraphMesReg } from '../../Models/IGraphMesReg';
import { Title } from '@angular/platform-browser';
import { MatGridListModule } from '@angular/material/grid-list';


@Component({
  selector: 'app-graphics',
  templateUrl: './graphics.component.html',
  styleUrls: ['./graphics.component.css'],
  standalone: true,
  imports: [CommonModule, MatGridListModule, NgFor],
})
export class GraphicsComponent implements OnInit, AfterViewInit {

  @ViewChild('canvas') canvas: ElementRef;
  @ViewChild('canvasGanancia') canvasGanancia: ElementRef;

  public chart: Chart;
  public chartGanancia: Chart;

  labels: string[] = [];
  dataCantReg: number[] = [];
  dataTotalGanado: number[] = [];
  dataTotalPendiente: number[] = [];
  dataTotal: number[] = [];
  dataConfiguration: any = [];

  labelsTipos: string[] = []
  dataCantidad: number[] = []
  dataConfigurationGanancia: any = [];

  data: any;
  constructor(private regService: RegularizacionService) {
    this.obtenerGraficos();
  }

  ngAfterViewInit(): void {
    this.initializeCharts();
  }
  ngOnInit(): void {
    this.chartdsds();
    this.dataConfiguration = [
      {
        label: 'Total',
        data: this.dataCantReg,
        fill: false,
        borderColor: 'rgba(75,192,192,1)',
        tension: 0.1,
        
      }
    ]

    this.dataConfigurationGanancia = [
      {
        label: 'Ganancias por Mes',
        data: this.dataCantidad,
        //fill: false,
        
      }
    ]
  }
  initializeCharts() {
    this.chart = new Chart('canvas', {
      type: 'line',
      data: {
        labels: this.labels,
        datasets: [
          {
            type: 'bar',
            label: 'Total a cobrar',
            //backgroundColor: Utils.transparentize(Utils.CHART_COLORS.red, 0.5),
            //borderColor: Utils.CHART_COLORS.red,
            data: this.dataTotal,
          },
          {
            type: 'bar',
            label: 'Total Cobrado',
            //backgroundColor: Utils.transparentize(Utils.CHART_COLORS.blue, 0.5),
            //borderColor: Utils.CHART_COLORS.blue,
            data: this.dataTotalGanado,
          },
          {
            type: 'bar',
            label: 'Total pendiente',
            //backgroundColor: Utils.transparentize(Utils.CHART_COLORS.blue, 0.5),
            //borderColor: Utils.CHART_COLORS.blue,
            data: this.dataTotalPendiente,
          },
          {
            type: 'line',
            label: 'Total de Reg.',
            //backgroundColor: Utils.transparentize(Utils.CHART_COLORS.green, 0.5),
            //borderColor: Utils.CHART_COLORS.green,
            fill: false,
            data: this.dataCantReg,
          }
        ]
      },
      options: {
        plugins: {
          title: {
            display: true,
            text: `Total de ingresos percibidos y regularizaciones hechas - ${new Date().getFullYear()}`
          },
        },
        scales: {
          x: {
            //type: 'time',
            display: true,
            offset: true,
            ticks: {
              source: 'data'
            },
            time: {
              unit: 'day'
            }
          }
        },
        interaction: {
          intersect: false,
        },
        responsive: true
      }
      
    });

    this.chartGanancia = new Chart('canvasGanancia', {
      type: 'pie' as ChartType,
      data: {
        labels: this.labelsTipos,
        datasets: [
          {
            label: 'Total',
            data: this.dataCantidad,
            backgroundColor: [
              'rgb(255, 99, 132)',
              'rgb(54, 162, 235)',
              'rgb(255, 205, 86)'
            ],
            hoverOffset: 3
            //fill: false,

          }
        ]
      },
      options: {
        plugins: {
          title: {
            display: true,
            text: `Total de regularizaciones Aprob., Rechazadas y subsanadas - ${new Date().getFullYear()}`
          }
        },
        interaction: {
          intersect: false,
        },
        responsive: true
      }
    });
  }
  chartdsds() {
    this.data = {
      labels: '',
      datasets: [{
          type: 'bar',
          label: 'Dataset 1',
          //backgroundColor: Utils.transparentize(Utils.CHART_COLORS.red, 0.5),
          //borderColor: Utils.CHART_COLORS.red,
          data: this.dataTotal,
        },
        {
          type: 'bar',
          label: 'Dataset 2',
          //backgroundColor: Utils.transparentize(Utils.CHART_COLORS.blue, 0.5),
          //borderColor: Utils.CHART_COLORS.blue,
          data: this.dataTotalGanado,
        },
        {
          type: 'bar',
          label: 'Dataset 3',
          //backgroundColor: Utils.transparentize(Utils.CHART_COLORS.blue, 0.5),
          //borderColor: Utils.CHART_COLORS.blue,
          data: this.dataTotalPendiente,
        },
        {
          type: 'line',
          label: 'Dataset 3',
          //backgroundColor: Utils.transparentize(Utils.CHART_COLORS.green, 0.5),
          //borderColor: Utils.CHART_COLORS.green,
          fill: false,
          data: this.dataCantReg,
      }]
    };
  }
  obtenerGraficos() {
    this.regService.getGraficoMesesReg().subscribe({
      next: (response: IGraphMesReg[]) => {
        response.forEach(data => {
          this.labels.push(data.mes);
          this.dataCantReg.push(data.cantidadRegularizaciones);
          this.dataTotalGanado.push(data.totalGanado);
          this.dataTotalPendiente.push(data.totalPendiente);
          this.dataTotal.push(data.total);
        })
        this.chart.update();
      },
      error: (error: HttpErrorResponse) => {
      }
    });
    this.regService.GetGananciaRegMes().subscribe({
      next: (response: IGraphGananciaMesReg[]) => {
        response.forEach(data => {
          this.labelsTipos.push(data.tipo);
          this.dataCantidad.push(data.cantidad);
        })
        this.chartGanancia.update();
      },
      error: (error: HttpErrorResponse) => {
      }
    });
  }
}
