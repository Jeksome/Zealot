﻿using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    private List<string> stories = new List<string>();

    public List<string> Stories
    {
        get { return stories; }
    }

    private void Start()
    {
        stories.Add("Оставляю ключ тебе. Вряд ли это поможет, но это единственное, чем я могу помочь. Оставляю твой посох тебе. Удачи.");
        stories.Add("Мы застряли здесь как крысы в ловушке. Мендоза исчез, скорее всего сбежал вместе с кузнецом, до того как демоны прорвались" +
            " во внутренний двор. Баррикады и замки их пока удерживают, но у нас больше нет запасов еды и воды, наш единственный шанс - попробовать прорваться с боем." +
            "Ни Верховный Инквизитор, ни Главный Надзиратель мне больше не указ, и их приказ я исполнять не стану. Я не святой, и о многом сожалею, но я не палач," +
            "и не стану лишать жизни заключенного без надлежащего суда. Пытки дали больше вопросов, чем ответов, и эти вопросы заставляют задуматься... " +
            "Если мне суждено умереть сегодня, пускай это не очистит меня от грехов, но я оставлю запасной" +
            "ключ в камере, а саму камеру закрою, чтобы у заключенного был хоть какой-то шанс. Да поможет нам всем Бог.");
        stories.Add("Этот упрямый кузнец начинает дейстовать мне на нервы. Сначала он уговаривал отправить его с гонцами в Собор к Верховному Инквизитору," +
            "теперь же говорит, что у него есть план, как выбраться отсюда, попросил зайти к нему, когда он закончит с мечами");
        stories.Add("Записка была спрятана в книге: Главный Надзиратель Мендоза, все готово.");
        stories.Add("Вчера к нам привели нового заключенного. Никогда не видел его на острове. Говорят, сам Верховый Инквизитор обвиняет его в том, " +
            "что он призвал демонов. Крики из пыточной было слышно всю ночь. Какая уже разница, отсюда нужно бежать. Только как, если все ключи у Мендозы?");
    }
}
