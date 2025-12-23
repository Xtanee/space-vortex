# SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
# SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
# SPDX-FileCopyrightText: 2025 Aviu00 <93730715+Aviu00@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

reagent-effect-guidebook-deal-stamina-damage =
    { $chance ->
        [1]
            { $deltasign ->
                [1] Наносит
               *[-1] Восстанавливает
            }
       *[other]
            { $deltasign ->
                [1] наносит
               *[-1] восстанавливает
            }
    } { $amount } { $immediate ->
        [true] немедленный
       *[false] постепенный
    } урон выносливости

reagent-effect-guidebook-stealth-entities = Маскирует живых существ поблизости.

reagent-effect-guidebook-change-faction = Меняет фракцию существа на {$faction}.

reagent-effect-guidebook-mutate-plants-nearby = Случайным образом мутирует растения поблизости.

reagent-effect-guidebook-dnascramble = Нарушает структуру ДНК существа.

reagent-effect-guidebook-change-species = Превращает цель в {$species}.

reagent-effect-guidebook-sex-change = Меняет пол существа.

reagent-effect-guidebook-immunity-modifier =
    { $chance ->
        [1] Изменяет
        *[other] изменяет
    } скорость выработки иммунитета на {$gainrate}, силу на {$strength} как минимум на {$time} {MANY("second", $time)}

reagent-effect-guidebook-disease-progress-change =
    { $chance ->
        [1] Изменяет
        *[other] изменяет
    } прогресс заболеваний типа {$type} на {$amount}

reagent-effect-guidebook-disease-mutate = Мутирует заболевания на {$amount}
