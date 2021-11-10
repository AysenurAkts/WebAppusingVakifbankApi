import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { DatePipe } from '@angular/common';
import { isNull } from '@angular/compiler/src/output/output_ast';

export class GoldPrice {
  constructor(
    public rateDate: number,
    public currencyCode: string,
    public productName: string,
    public saleRate: string,
    public isin: string,
    public purchaseRate: string
  ) {
  }
}

//datetimepicker'da gösterilen tarih için formatlama işlemleri yapılır
export const MY_DATE_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY',
  },
  display: {
    dateInput: 'YYYY-MM-DD',
    monthYearLabel: 'MMMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY'
  },
};

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [
    { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS }
  ]
})

export class AppComponent {

  goldPrices! : GoldPrice[];

  events: string[] = [];

  tarih? :Date;

  datepipe :any

  minDate?: Date;
  maxDate?: Date;

  //datetimepicker'da kullanıcının girdiği tarih bilgisi alınır
  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    this.events.push(`${type}: ${event.value}`);
    this.tarih = (event.value as Date);
    console.log(this.tarih.toISOString());
  }
  
  constructor(private httpClient : HttpClient, private modalService:NgbModal) {
      const currentYear = new Date().getFullYear();
      this.minDate = new Date(currentYear - 20, 0, 1);
      this.maxDate = new Date(currentYear, 9, 9);
      
   }
  title = 'goldprice-angular';

  ngOnInit(): void {      
  }

  ngAfterContentInit(): void {      
  if(this.goldPrices.length == 0)
    window.alert("Kayıt Bulunamadı" + this.goldPrices.length)
  }

  //altın fiyat bilgileri oluşturulan servise get request yapılarak response'da alınır
  getGoldPrices(){ 
    this.datepipe = new DatePipe('en-Us').transform(this.tarih, 'yyyy-MM-dd', 'GMT+3');
    this.httpClient.get<any>('http://localhost:55176/api/GoldPriceDetail/'+ this.datepipe)
      .subscribe(
       (response) => {
        console.log(this.goldPrices);
        this.goldPrices = response;
      }
      );
  }
}