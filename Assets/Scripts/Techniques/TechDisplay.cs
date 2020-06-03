using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechDisplay : MonoBehaviour
{
    // creating a variable of the other class we created and calling it "_techs" we can then access things from the other class by using _techs.xxx.
    // this is similar to when an instance of a class is made, setting instance = this and then calling the class from another class
    public Techs _techs;

    public Text _nameText;
    public Text _descriptionText;

    public Image _artworkImage;

    public Text _manaText;
    public Text _attackText;

    // Start is called before the first frame update
    void Start()
    {
        _nameText.text = _techs.name;
        _descriptionText.text = _techs.description;

        _artworkImage.sprite = _techs.artwork;

        _manaText.text = _techs.manaCost.ToString();
        _attackText.text = _techs.attack.ToString();
    }

}
