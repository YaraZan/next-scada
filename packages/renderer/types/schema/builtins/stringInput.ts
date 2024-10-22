import type {Node, NodeAspect, NodeProperties, NodeProperty, NodeStyles} from '../node';

type StringInputPropertyKeys = 'value';
type StringInputProperties = Record<StringInputPropertyKeys, NodeProperty> & NodeProperties;
type StringInputStyleKeys = 'placeholder' | 'color';
type StringInputStyles = Record<StringInputStyleKeys, NodeAspect> & NodeStyles;

export interface StringInput extends Node {
  properties?: StringInputProperties;
  styles?: StringInputStyles;
}
