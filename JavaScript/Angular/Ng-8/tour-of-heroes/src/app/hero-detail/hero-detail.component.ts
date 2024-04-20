import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HeroService } from '../hero.service';
import { Location } from '@angular/common';
import { Hero } from '../hero';

@Component({
  selector: 'app-hero-detail',
  templateUrl: './hero-detail.component.html',
  styleUrls: ['./hero-detail.component.css']
})
export class HeroDetailComponent implements OnInit {

  selectedHero:Hero;

  constructor(private route: ActivatedRoute, private location: Location, private heroService:HeroService) { }

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.heroService.getHero(id).subscribe(hero => this.selectedHero = hero);
  }

  goBack(): void{
    this.location.back();
  }

  save(): void {
    this.heroService.updateHero(this.selectedHero)
      .subscribe(() => this.goBack());
  }

}
