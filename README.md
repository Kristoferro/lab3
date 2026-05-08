# Laboratorium 3: Obliczenia wielowątkowe w technologii .NET

## Opis projektu
Projekt został zrealizowany w ramach przedmiotu **Platformy Programistyczne .NET i Java**. Celem laboratorium było zapoznanie się z podstawami przetwarzania wielowątkowego w technologii .NET oraz analiza przyspieszenia obliczeń poprzez zrównoleglenie kodu.

## Zadania
Repozytorium zawiera implementację następujących zadań:
1.  **Biblioteka Parallel**: Analiza przyspieszenia mnożenia macierzy względem podejścia sekwencyjnego przy użyciu wysokopoziomowej biblioteki `Parallel`.
2.  **Klasa Thread**: Wielowątkowe mnożenie macierzy z wykorzystaniem niskopoziomowej klasy `Thread` wraz z analizą wydajności.
3.  **Przetwarzanie obrazów**: (W trakcie/Implementacja) Wielowątkowe nakładanie filtrów na obrazy w aplikacji okienkowej Windows Forms.

## Technologie
* Język: C#
* Platforma: .NET 8.0
* Środowisko: Visual Studio 2022

## Struktura projektu
* **Program1**: Aplikacja konsolowa służąca do przeprowadzania benchmarków mnożenia macierzy. Porównuje ona wydajność biblioteki `Parallel` oraz klasy `Thread` dla różnych rozmiarów macierzy i liczby wątków.

## Analiza wyników
W ramach zadań 1 i 2 przeprowadzono testy wydajnościowe. Zaobserwowano, że:
* Dla małej liczby wątków (1-3) klasa `Thread` często wykazuje mniejszy narzut konfiguracyjny niż biblioteka `Parallel`.
* Przy większej liczbie wątków i dużych macierzach, biblioteka `Parallel` lepiej zarządza zasobami dzięki wykorzystaniu puli wątków (ThreadPool).
* Najlepsze efekty zrównoleglenia są widoczne przy macierzach o rozmiarach 100.
