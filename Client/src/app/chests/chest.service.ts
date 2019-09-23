import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ApiHelper } from '../shared/Helper';

@Injectable({
  'providedIn': 'root'
})
export class ChestService {
  private _chestApi = `${ApiHelper.clashRoyaleApi}/chests`;
  private _chestImageApi = `${ApiHelper.clashRoyaleImageApi}/chests/`;

  constructor(private http: HttpClient) { }

  /**
   * Retrieve all chests
   */
  getChests(): Observable<Chest[]> {
    return this.http.get<Chest[]>(this._chestApi)
                .pipe(
                  map(data => {
                    const modifiedData: Chest[] = [];

                    data.forEach(chest => {
                      chest.imageUrl = this.setChestImage(chest);
                      modifiedData.push(chest);
                    });

                    return modifiedData;
                  }),
                  catchError(this.handleError)
                );
  }

  /**
   * Retrieve chest base on chest id
   * @param id Chest id to search
   */
  getChestById(id: string): Observable<Chest> {
    return this.http.get<Chest>(`${this._chestApi}/${id}`)
      .pipe(
        map(data => {
          data.imageUrl = this.setChestImage(data);
          return data;
        }),
        catchError(this.handleError)
      );
  }

  setChestImage(chest: Chest): string {
    let imageName = '';

    imageName = chest.idName.substring(0, chest.idName.length - 2).replace('\'', '');

    return `${this._chestImageApi}${imageName}.png`;
  }

  private handleError(err: HttpErrorResponse) {
    console.log('ChestService: ' + err.message);
    return Observable.throw(err.message);
  }
}
