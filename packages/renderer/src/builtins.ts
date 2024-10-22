import type {Node} from 'types/schema/node';

export const nodes: Record<string, Node> = {
  LabelView: {
    name: 'Label',
    type: 'Label',
    styles: {
      width: {component: 'NumberInput', value: '2px'},
      height: {component: 'NumberInput', value: '20px'},
      fontSize: {component: 'NumberInput', value: '14px'},
      fontWeight: {
        component: 'OptionPicker',
        value: 400,
        options: [100, 200, 300, 400, 500, 600, 700, 800, 900],
      },
      color: {component: 'ColorPicker', value: '#000000'},
    },
    properties: {
      visibility: {component: 'BooleanInput', value: true, dataSource: null},
      rotation: {component: 'BooleanInput', value: '0deg'},
      text: {component: 'StringInput', value: 'Label', dataSource: null},
    },
  },

  ImageView: {
    name: 'Image',
    type: 'ImageView',
    styles: {
      width: {component: 'NumberInput', value: '300px'},
      height: {component: 'NumberInput', value: '300px'},
      objectFit: {
        component: 'OptionPicker',
        value: 'cover',
        options: ['contain', 'cover'],
      },
    },
    properties: {
      visibility: {component: 'BooleanInput', value: true, dataSource: null},
      rotation: {component: 'BooleanInput', value: '0deg'},
      backgroundImage: {component: 'UploadField', value: null},
    },
  },

  NumberInput: {
    name: 'Number',
    type: 'NumberField',
    styles: {
      width: {component: 'NumberInput', value: '100px'},
      height: {component: 'NumberInput', value: '20px'},
      placeholder: {component: 'StringInput', value: 'Placeholder'},
      color: {component: 'ColorPicker', value: '#000000'},
    },
    properties: {
      visibility: {component: 'BooleanInput', value: true, dataSource: null},
      rotation: {component: 'BooleanInput', value: '0deg'},
      value: {component: 'NumberInput', value: null, dataSource: null},
    },
  },

  StringInput: {
    name: 'String',
    type: 'StringField',
    styles: {
      width: {component: 'NumberInput', value: '100px'},
      height: {component: 'NumberInput', value: '20px'},
      placeholder: {component: 'StringInput', value: 'Placeholder'},
      color: {component: 'ColorPicker', value: '#000000'},
    },
    properties: {
      visibility: {component: 'BooleanInput', value: true, dataSource: null},
      rotation: {component: 'BooleanInput', value: '0deg'},
      value: {component: 'StringInput', value: null, dataSource: null},
    },
  },

  BooleanInput: {
    name: 'Boolean',
    type: 'BooleanToggle',
    styles: {
      width: {component: 'NumberInput', value: '50px'},
      height: {component: 'NumberInput', value: '20px'},
    },
    properties: {
      visibility: {component: 'BooleanInput', value: true, dataSource: null},
      rotation: {component: 'BooleanInput', value: '0deg'},
      value: {component: 'BooleanInput', value: false, dataSource: null},
    },
  },

  ArrayInput: {
    name: 'Array',
    type: 'ArrayChart',
    styles: {
      width: {component: 'NumberInput', value: '150px'},
      height: {component: 'NumberInput', value: '150px'},
    },
    properties: {
      visibility: {component: 'BooleanInput', value: true, dataSource: null},
      rotation: {component: 'BooleanInput', value: '0deg'},
      value: {component: 'ArrayInput', value: false, dataSource: null},
    },
  },
};

export default nodes;
