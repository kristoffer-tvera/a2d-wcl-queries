document.addEventListener('DOMContentLoaded', function () {
    let options = {
        'inDuration': 250,
        'outDuration': 200
    };

    let elems = document.querySelectorAll('.sidenav');
    let instances = M.Sidenav.init(elems, options);
});


function PopulateEventsForFight(fightCode, filterExpression, startTime, endTime, sender) {
    fetch('/api/getevents?fightCode=' + fightCode + '&filterExpression=' + filterExpression + '&startTime=' + startTime + '&endTime=' + endTime).then(function (response) {
        return response.json();
    }).then(function (events) {

        sender.innerHTML = '';
        for (var i = 0; i < events.length; i++) {
            let card = document.createElement('div');
            card.className = 'card blue-grey darken-1';

            let content = document.createElement('div');
            content.className = 'card-content white-text';

            let cardTitle = document.createElement('span');
            cardTitle.className = 'card-title';

            let source = players.find( player  => player.id == events[i].sourceID);
            let target = players.find(player => player.id == events[i].targetID);

            let timeInSeconds = (events[i].timestamp - startTime) / 1000;
            let minutes = Math.floor(timeInSeconds / 60);
            let seconds = Math.floor(timeInSeconds - (minutes * 60));
            let milliseconds = Math.floor((timeInSeconds - (minutes * 60) - seconds) * 1000);

            cardTitle.innerText = `${source.name}'s IQD gave ${target.name} ${events[i].resourceChange} mana at ${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}.${milliseconds}`;

            content.appendChild(cardTitle);
            card.appendChild(content);

            sender.appendChild(card);
        }

        sender.querySelector('div.progress');

    }).catch(function (err) {
        console.warn('Error: ', err);
    });
}

(function () {
    let elements = document.querySelectorAll('[data-event]');
    if (elements && elements.length > 0) {
        for (let i = 0; i < elements.length; i++) {
            let fightCode = elements[i].getAttribute('data-fight-code');
            let filterExpression = elements[i].getAttribute('data-filter-expression');
            let startTime = elements[i].getAttribute('data-start-time');
            let endTime = elements[i].getAttribute('data-end-time');
            PopulateEventsForFight(fightCode, filterExpression, startTime, endTime, elements[i]);
        }
    }
})();