import { IngredientAbstract } from './ingredient-abstract.model';

export class Ingredient extends IngredientAbstract{
    ingredientId: number;
    ingredientCategory: string;
    ingredientCategoryId: number;
}
